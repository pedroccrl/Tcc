# pylint: disable=C0111
# pylint: disable=C0103

from pickle import load
import re
import nltk
from pymongo import MongoClient
import pre_processamento
import RemovedorDeAcentos

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')
stop_words = nltk.corpus.stopwords.words('portuguese')

tipo_log = ['rua', 'praça', 'alameda', 'rodovia', 'beco', 'estrada', 'largo', 'ramal', 'travessa']
localidades = ['praia', 'bairro'] + tipo_log

def verifica_logradouros(id_cidade):
    c_col = db['comentarios_original']
    comentarios = c_col.find({'IdCidade': id_cidade, '$where': '!this.TemLocalidade || this.TemLocalidade == true'})
    
    bairros = db['bairro'].find({'IdCidade': id_cidade})
    for c in comentarios:
        logradouros = []
        logradouros_encontrados = []
        coment = c['message']
        coment = pre_processamento.CorrigirTexto(coment)
        palavras = nltk.word_tokenize(coment.lower(), language='portuguese') # separar o comentario em array de palavras
        palavras = [p for p in palavras if p not in stop_words] # retirar stop words
        locais = list(set([p for p in palavras if p in tipo_log])) # cria array com os locais unicos nos comentarios
        if len(locais) > 0: 
            for l in locais:
                logradouros += db['logradouros'].find({'IdCidade': id_cidade, 'Tipo': re.compile(l, re.IGNORECASE)})
            for logr in logradouros:
                nome_log = logr['Nome']
                p = re.compile(r'[.-].+')
                nome_log = p.sub('', nome_log).strip()
                p = re.compile(nome_log+r'\W', re.IGNORECASE)
                achou = p.match(coment)
                if achou:
                    logradouros_encontrados.append(logr)
            c['TemLogradouro'] = True
            c['Logradouros'] = logradouros_encontrados
        else:
            c['TemLogradouro'] = False
        c_col.update_one({'_id': c['_id']}, {'$set': c})
        v =1
verifica_logradouros(2)
