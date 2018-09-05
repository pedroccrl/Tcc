# pylint: disable=C0111
# pylint: disable=C0103
import time
import mongo
import nltk
from nltk.corpus import mac_morpho
import pre_processamento
import spell_check

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

col_c = mongo.db["comentarios_original"]
comentarios_o = [c for c in col_c.find({'$where' : 'this.data = null ||'})]

col = mongo.db['palavras']
col_nexist = mongo.db['palavras_unk']

palavras = []
for p in col.find():
    palavras.append(p['Palavra'])

palavras_unk = []
tempo = time.time()

def VerificarPalavras(comentarios):
    for c in comentarios:
        c = pre_processamento.CorrigirComentario(c)
        coment = c['corrigido']
        sents = sent_tokenizer.tokenize(coment)
        unk = []
        unk_c = []
        for sent in sents:
            words = nltk.word_tokenize(sent)
            for word in words:
                if word.lower() not in palavras:
                    c_word = spell_check.correction(word)
                    coment = coment.replace(word, c_word)
                    unk.append(word)
                    if c_word not in palavras:
                        palavras_unk.append(word)
                        unk_c.append(c_word)
        c['palavras_unk'] = unk
        c['palavras_unk_c'] = unk_c
        col_c.update_one({'_id':c['_id']}, {"$set":c}, upsert=False)

VerificarPalavras(comentarios_o)
