# pylint: disable=C0111
# pylint: disable=C0103
from pymongo import MongoClient

client = MongoClient('localhost', 27017)
db = client['dados_tcc']


