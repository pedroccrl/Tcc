# pylint: disable=C0111
# pylint: disable=C0103
import mongo as m
import nltk

def freq_palavras():
    comentarios = m.comentarios_tagged()
    palavras = []
    for com in comentarios:
        for pos_tag in com['pos_tag']:
            for sent in pos_tag:
                c = sent[0]
                tag = sent[1]
                if tag in 'NPROP':
                    palavras.append(c)
    freq = nltk.FreqDist(palavras)
    print(freq.most_common(50))

def freq_localidade(localidade, n=4):
    comentarios = m.comentarios_tagged()
    localidades = []
    for com in comentarios:
        for pos_tag in com['pos_tag']:
            for p in pos_tag:
                if p[0] == localidade:
                    i = pos_tag.index(p)
                    local = []
                    local = [l[0].lower() for l in pos_tag[i:i+n] if l[1] == 'NPROP' or l[1] == 'N' or l[1] == 'PREP']
                    #print(local)
                    if (len(local) > 1):
                        localidades.append(local)
    vet = []
    for local in localidades:
        frase = ' '.join([l for l in local[1:]])
        vet.append(frase)
    freq = nltk.FreqDist(vet)
    print(freq.most_common(20))
    for l in localidades:
        print(l)

# freq_localidade('rua')
freq_palavras()

