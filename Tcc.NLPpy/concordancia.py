from nltk.corpus import mac_morpho

def concordance(word, context=30):
    for sent in mac_morpho.sents():
        if word in sent:
            pos = sent.index(word)
            left = ' '.join(sent[:pos])
            right = ' '.join(sent[pos+1:])
            print('%*s %s %-*s' %\
                    (context, left[-context:], word, context, right[:context]))

def pos_concordance(word):
    for sent_pos in mac_morpho.tagged_sents():
        sent = [s for s in sent_pos if word in s[0]]
while True:
    palavra = input()
    concordance(palavra)
