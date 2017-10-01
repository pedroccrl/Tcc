# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

defaultTagger = nltk.DefaultTagger('N')
resultado = defaultTagger.evaluate(sents.tsents)
print(resultado*100.0)

# Precisão foi de 20.208%
# 01/10/2017 13:59