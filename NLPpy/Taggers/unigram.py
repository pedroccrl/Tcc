# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

tagger = nltk.UnigramTagger(sents.sentTreino)
resultado = tagger.evaluate(sents.sentTeste)
print(resultado*100.0)

# Precisão foi de 79.993%
# 01/10/2017 14:08