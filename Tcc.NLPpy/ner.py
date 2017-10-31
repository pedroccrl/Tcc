# pylint: disable=C0111
# pylint: disable=C0103
import re
import nltk
from pymongo import MongoClient
import RemovedorDeAcentos
import mongo

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

logradouros = db['logradouros']

# def pos_tag(comentario):
#     sents = sent_tokenizer.tokenize(comentario)
#     pos_tag_sents = []
#     for sent in sents:
#         words = nltk.word_tokenize(sent)
#         tagged = combinado.FinalTagger.tag(words)

#         chunkGram = r""" Rua: {} """ 

#         pos_tag_sents.append(tagged)
#     # print(pos_tag_sents)
#     return pos_tag_sents

def NomearLocalidades(comentarios):
    i = 0
    for c in comentarios:
        i += 1
        print(str(i) + '/' +  str(len(comentarios)), end='\r')
        msg = c['corrigido']
        p = re.compile(r'\s(da|do|de|das|dos)\s', re.IGNORECASE)
        msg = p.sub(' ', msg)
        p = re.compile(r'\s*')
        msg = p.sub(' ', msg)
        msg = RemovedorDeAcentos.remover_acentos(msg)
        for r in logradouros.find():
            
            rua = r['Nome']
            p = re.compile(r'-.*', re.IGNORECASE)
            rua = p.sub('', rua)
            p = re.compile(r'\s*')
            rua = p.sub(' ', rua)
            rua = RemovedorDeAcentos.remover_acentos(rua)
            
            p = re.compile(rua, re.IGNORECASE)
            achou = p.match(msg)
            if achou:
                logradouros.update_one({'_id':r['_id']},{'$push': {'comentarios': c} })
                print('achou\n')

NomearLocalidades(mongo.comentarios_original())
