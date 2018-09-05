# pylint: disable=C0111
# pylint: disable=C0103
from nltk.corpus import mac_morpho
import nltk

tags = [t for s in mac_morpho.tagged_sents() for (p, t) in s]
frequencia = nltk.FreqDist(tags)

print(frequencia.most_common(5))
