# pylint: disable=C0111
# pylint: disable=C0103
from pickle import load
import nltk
from pymongo import MongoClient
import pre_processamento
import verifica_tema_nucleo

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

# carregando o tagger
input = open('mac_morpho.pkl', 'rb')
tagger = load(input)
input.close()
sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

def filtra_tags(tags):
    new_tags = []
    # print(tags)
    for (palavra, sintaxe) in tags:
        if sintaxe == 'NPROP' or sintaxe == 'N':
            new_tags.append(palavra)
    # print(new_tags)

    stopwords = nltk.corpus.stopwords.words('portuguese')
    
    return [p for p in new_tags if p.lower() not in stopwords and len(p) > 2]

def verificar(id_cidade):
    comentarios_col = db['comentarios_original']
    comentarios = comentarios_col.find({'IdCidade': id_cidade})
    
    nomes = []
    qualidades = []

    for c in comentarios:
        texto = c['message']
        tema = verifica_tema_nucleo.verificar_texto(texto)
        nomes += tema['nomes']
        qualidades += tema['qualidades']
        comentarios_col.update_one({'_id': c['_id']}, {'$set': {'tema': tema}})
    
    freq_nome = nltk.FreqDist(nomes)
    freq_qual = nltk.FreqDist(qualidades)
    #freq = nltk.FreqDist(filtra_tags([t for v in tags for t in v]))
    #comum = freq.most_common(80)

    print(freq_nome.most_common(100))
    print(freq_qual.most_common(40))

    cidade_col = db['cidades']
    cidade_col.update_one({'id_cidade':id_cidade}, {'$set': {'id_cidade': id_cidade, 'temas': freq_nome.most_common(100), 'qualidades': freq_qual.most_common(40)}}, upsert=True)

verificar(2)


