# pylint: disable=C0111
# pylint: disable=C0103
import combinado
import nltk
from pymongo import MongoClient

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

def pos_tag(comentario):
    sents = sent_tokenizer.tokenize(comentario)
    pos_tag_sents = []
    for sent in sents:
        words = nltk.word_tokenize(sent)
        tagged = combinado.FinalTagger.tag(words)

        chunkGram = r""" Rua: {} """ 

        pos_tag_sents.append(tagged)
    # print(pos_tag_sents)
    return pos_tag_sents