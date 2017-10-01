# pylint: disable=C0111
# pylint: disable=C0103
from nltk.corpus import mac_morpho
import nltk

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

tsents = mac_morpho.tagged_sents()
tamCorpus = int(len(tsents)*0.8)
sentTreino = tsents[:tamCorpus]
sentTeste = tsents[tamCorpus:]
