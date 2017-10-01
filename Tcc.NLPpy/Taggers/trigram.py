# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

tagger = nltk.TrigramTagger(sents.sentTreino)
resultado = tagger.evaluate(sents.sentTeste)
print(resultado*100.0)

# Precisão foi de 10.331%
# 01/10/2017 14:20