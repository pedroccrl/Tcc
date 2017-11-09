# pylint: disable=C0111
# pylint: disable=C0103
# pylint: disable=C0326
# pylint: disable=C0303

import re

pontos = ['.',',','!','?',';']
acentos = ['á','é','í','ó','ú','à','è','ì','ò','ù','ã','ẽ','ĩ','õ','ũ','â','ê','î','ô','û']
s_acentos = ['a','e','i','o','u','a','e','i','o','u','a','e','i','o','u','a','e','i','o','u']

abreviacao = [
    ('vc', 'você'),
    ('vcs', 'vocês'), 
    (' q ', ' que '), 
    # ('p', 'para'), 
    ('td', 'tudo'), 
    ('tds', 'todos'), 
    ('qdo', 'quando'),
    ('sdds', 'saudades'),
    ('blz', 'beleza'),
    ('vlw', 'valeu'),
    ('flw', 'falou'),
    (' pq ', ' porque '),
    (' ta ', ' está '),
    (' ta ', ' está '),
    ('tmj', 'estamos juntos'),
    (' tbm ', ' também '),
    (' ja ', ' já ')
]

def CorrigirComentario(comentario):
    comentario['corrigido'] = CorrigirTexto(comentario['message'])
    return comentario

def to_lower(match):
    return match.group(0).lower()

def CorrigirTexto(texto):
    p = re.compile(r'http://|www\S*', re.IGNORECASE) # retirar links
    texto = p.sub('', texto)
    p = re.compile(r'(\w)\1{2,}', re.IGNORECASE) # retirar mais que 2 letras repetidas
    texto = p.sub(r'\1', texto)
    p = re.compile(r'([!.?])\1{1,}', re.IGNORECASE) # retirar mais que 1 ponto repetido e adicionar um espaço após
    texto = p.sub(r'\1 ', texto) 
    p = re.compile(r' ([!?.])') # retirar um espaço antes de pontuação
    texto = p.sub(r'\1', texto)
    p = re.compile(r'(\w)(ao)\s', re.IGNORECASE) # colocar ~ numa palavra terminada em ao
    texto = p.sub(r'\1ão ', texto)
    texto = re.sub(r'[A-Z]{2,}|[a-z][A-Z]+', to_lower, texto) # substitui mais uma palavra maiscula para minuscula

    for (abr, s_abr) in abreviacao:
        p = re.compile(abr, re.IGNORECASE)
        texto = p.sub(s_abr, texto)
    return texto

#CorrigirTexto('Oi como vc VAI RiO DaS OsTrAs')
