# pylint: disable=C0111
# pylint: disable=C0103

from pickle import load
import nltk
from pymongo import MongoClient
import pre_processamento

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

# carregando o tagger
input = open('mac_morpho.pkl', 'rb')
tagger = load(input)
input.close()
sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

def verifica_logradouros(id_cidade):
    logradouros_col = db['logradouros']
    bairros_col = db['bairro']
    comentarios_col = db['comeentarios_original']
