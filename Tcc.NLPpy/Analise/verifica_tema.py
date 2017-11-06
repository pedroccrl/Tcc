# pylint: disable=C0111
# pylint: disable=C0103
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

def filtra_tags(tags):
    new_tags = []
    # print(tags)
    for (palavra, sintaxe) in tags:
        if sintaxe == 'NPROP' or sintaxe == 'N':
            new_tags.append(palavra)
    # print(new_tags)

    stopwords = nltk.corpus.stopwords.words('portuguese')
    
    return [p for p in new_tags if p.lower() not in stopwords and len(p) > 2]

def verificar(id_cidade):
    comentarios_col = db['comentarios_original']
    comentarios = comentarios_col.find({'IdCidade': id_cidade})
    tags = []
    nomes = []
    qualidades = []
    logradouros = []
    for c in comentarios:
        c = pre_processamento.CorrigirComentario(c)
        sents = sent_tokenizer.tokenize(c['corrigido'])
        for sent in sents:
            palavras = nltk.word_tokenize(sent, language='portuguese')
            tagged = tagger.tag(palavras)

            chunkGram = """
                           Nome2: {<N|NPROP>+<ADJ>*<N|NPROP>*}
                           Relacao: {<V.*>}
                           Qualidade : {<ADJ>+} 
                           Logradouro: {<N|NPROP>+<PREP>*<N|NPROP>+}
                        """
            chunkParser = nltk.RegexpParser(chunkGram)
            chunked = chunkParser.parse(tagged)
            for subtree in chunked.subtrees():
                if subtree.label() in 'Nome2':
                    nome = [n for (n,t) in subtree.leaves() if len(n) > 2]
                    nomes.append(' '.join(nome))
                if subtree.label() == 'Qualidade':
                    qual = [n for (n,t) in subtree.leaves() if len(n) > 2]
                    qualidades.append(' '.join(qual))
                if subtree.label() == 'Logradouro':
                    logr = [n for (n,t) in subtree.leaves() if len(n) > 2]
                    logradouros.append(' '.join(logr))
                    print(logr)
            #print(str(subtree.label()) + str(subtree.leaves()))
            # print(tagged)
            tags.append(tagged)

    freq_nome = nltk.FreqDist(nomes)
    freq_qual = nltk.FreqDist(qualidades)
    freq_logra = nltk.FreqDist(logradouros)
    freq = nltk.FreqDist(filtra_tags([t for v in tags for t in v]))
    comum = freq.most_common(80)

    print(freq_nome.most_common(100))
    print(freq_qual.most_common(40))
    print(freq_logra.most_common(40))

    cidade_col = db['cidades']
    cidade_col.update_one({'id_cidade':id_cidade}, {'$set': {'id_cidade': id_cidade, 'temas': comum}}, upsert=True)

verificar(2)


