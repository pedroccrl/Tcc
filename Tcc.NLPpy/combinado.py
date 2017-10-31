# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import sents

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

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

def pos_tag(comentario):
    sents = sent_tokenizer.tokenize(comentario)
    pos_tag_sents = []
    for sent in sents:
        words = nltk.word_tokenize(sent)
        tagged = FinalTagger.tag(words)
        pos_tag_sents.append(tagged)
    # print(pos_tag_sents)
    return pos_tag_sents

while True:
    sent = input()
    print(pos_tag(sent))

# resultado = unigramTagger.evaluate(sents.sentTeste)
# print(resultado*100.0)

# Precisão foi de 81.521% com regex (prep), default(N) e unigram 
# 01/10/2017 14:40

# Precisão foi de 81.545% com regex (prep), default(N), unigram e bigram
# 03/10/2017 07:28

# Precisão foi de 81.553% com regex (prep), default(N), unigram e bigram
# 01/10/2017 14:33