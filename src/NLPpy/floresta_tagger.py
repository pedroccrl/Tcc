from nltk.corpus import floresta
import nltk

tags = []
for sent in floresta.tagged_sents():
    for (palavra, tag) in sent:
        tags.append(tag)
freq = nltk.FreqDist(tags)
print(freq.most_common(50))