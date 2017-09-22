# pylint: disable=C0111
# pylint: disable=C0103
from pymongo import MongoClient

client = MongoClient('localhost', 27017)
db = client['dados_tcc']
comentarios_original_coll = db['comentarios_original']
comentarios_postag_coll = db['comentarios_postagged']

def comentarios_original():
    comentarios = []
    for comm in comentarios_original_coll.find():
        comentarios.append(comm)
    return comentarios

def comentarios_tagged():
    comentarios = []
    for comm in comentarios_postag_coll.find():
        comentarios.append(comm)
    return comentarios

def adicionar_postag(comm_postagged):
    comentarios_postag_coll.insert_one(comm_postagged)
