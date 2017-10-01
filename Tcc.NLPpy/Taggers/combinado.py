# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

defaultTagger = nltk.DefaultTagger('N')
patterns = [
    (r'(da|do|de|das|dos)$', 'PREP')
]
regexTagger = nltk.RegexpTagger(patterns, backoff=defaultTagger)
unigramTagger = nltk.UnigramTagger(sents.sentTreino, backoff=regexTagger)
resultado = unigramTagger.evaluate(sents.sentTeste)
print(resultado*100.0)

# Precisão foi de 81.521% com regex (prep), default(N) e unigram 
# 01/10/2017 14:40