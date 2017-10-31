# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents
from pickle import dump

defaultTagger = nltk.DefaultTagger('N')
patterns = [
    (r'(da|do|de|das|dos)$', 'PREP'),
    (r'.*ndo$', 'V-GER')
]
regexTagger = nltk.RegexpTagger(patterns, backoff=defaultTagger)
unigramTagger = nltk.UnigramTagger(sents.sentTreino, backoff=regexTagger)
bigramTagger = nltk.BigramTagger(sents.sentTreino, backoff=unigramTagger)
trigramTagger = nltk.TrigramTagger(sents.sentTreino, backoff=bigramTagger)

FinalTagger = trigramTagger

output = open('mac_morpho.pkl', 'wb')
dump(FinalTagger, output, -1)
output.close()
# resultado = unigramTagger.evaluate(sents.sentTeste)
# print(resultado*100.0)

# Precisão foi de 81.521% com regex (prep), default(N) e unigram 
# 01/10/2017 14:40

# Precisão foi de 81.545% com regex (prep), default(N), unigram e bigram
# 03/10/2017 07:28

# Precisão foi de 81.553% com regex (prep), default(N), unigram e bigram
# 01/10/2017 14:33