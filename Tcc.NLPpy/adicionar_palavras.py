# pylint: disable=C0111
# pylint: disable=C0103
import mongo
import nltk
from nltk.corpus import mac_morpho

col_p = mongo.db['palavras']

palavras = [p for p in mac_morpho.words()]

freq = nltk.FreqDist(palavras)

for p in freq:
    col_p.insert_one({'Palavra':p.lower()})