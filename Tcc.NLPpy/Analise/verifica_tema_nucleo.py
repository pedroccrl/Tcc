# pylint: disable=C0111
# pylint: disable=C0103

# Script nucleo para verificar temas e POS-Tagging de uma frase

from pickle import load
import nltk
from pymongo import MongoClient
import pre_processamento

client = MongoClient('localhost', 27017)
db = client['dados_tcc']

# carregando o tagger
input = open('mac_morpho.pkl', 'rb')
tagger = load(input)
input.close()
sent_tokenizer = nltk.data.load('tokenizers/punkt/portuguese.pickle')

def verificar_texto(texto):
    tags = []
    nomes = []
    qualidades = []
    texto = pre_processamento.CorrigirTexto(texto.lower()) # corrige o texto antes de analisar
    sents = sent_tokenizer.tokenize(texto) # separa o texto em sentenças (frases)
    for sent in sents:
        palavras = nltk.word_tokenize(sent, language='portuguese') # separa a frase em palavras
        tagged = tagger.tag(palavras) # classifica as palavras em POS-Tagging
        tags.append(tagged)
        chunkGram = """
                    Sujeito: {<N.*>+<PREP.*>*<KS>*<ADJ>*<N.*>*<PREP.*>*<KS>*<ADJ>*<N.*>*}
                    Relacao: {<V.*>*}
                    Qualidade: {<ADJ>*}
                    """
        chunkParser = nltk.RegexpParser(chunkGram)
        chunked = chunkParser.parse(tagged)
        #chunked.draw()
        for subtree in chunked.subtrees():
            if subtree.label() == 'Sujeito':
                nome = [n for (n, t) in subtree.leaves() if len(n) >= 2]
                nomes.append(' '.join(nome))
            if subtree.label() == 'Qualidade':
                qual = [n for (n, t) in subtree.leaves() if len(n) >= 2]
                qualidades.append(' '.join(qual))
    tema = {}
    tema['nomes'] = nomes
    tema['qualidades'] = qualidades
    tema['pos_tags'] = tags
    return tema
