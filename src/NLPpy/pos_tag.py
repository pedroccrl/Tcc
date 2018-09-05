# pylint: disable=C0111
# pylint: disable=C0103
import nltk
import mongo as m
from nltk.corpus import mac_morpho

sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

tsents = mac_morpho.tagged_sents()
train = tsents[0:1000]
test = tsents[:100]

tagger0 = nltk.DefaultTagger('N')
tagger1 = nltk.UnigramTagger(train, backoff=tagger0)
tagger2 = nltk.BigramTagger(train, backoff=tagger1)

def pos_tag(comentario):
    sents = sent_tokenizer.tokenize(comentario)
    pos_tag_sents = []
    for sent in sents:
        words = nltk.word_tokenize(sent)
        tagged = tagger2.tag(words)
        pos_tag_sents.append(tagged)
    # print(pos_tag_sents)
    return pos_tag_sents

def tag_comentarios_original():
    for comm in m.comentarios_original():
        postag_sent = pos_tag(comm['message'])
        comm['pos_tag'] = postag_sent
        m.adicionar_postag(comm)

# tag_comentarios_original()

while True:
    sentenca = input()
    print(pos_tag(sentenca))
