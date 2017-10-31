# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

patterns = [
    (r'(da|do|de|das|dos)$', 'PREP'), # Preposições
    (r'.*ndo$', 'V-GER') # Gerundios
]
defaultTagger = nltk.DefaultTagger('N')
regexTagger = nltk.RegexpTagger(patterns, backoff=defaultTagger)
resultado = regexTagger.evaluate(sents.sentTeste)
print(resultado*100.0)

# Precisão foi de 23.130%
# 01/10/2017 14:34