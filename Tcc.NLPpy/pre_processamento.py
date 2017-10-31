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
    ('ta ', 'está '),
    ('tmj', 'estamos juntos')
]

def CorrigirComentario(comentario):
    msg = comentario['message']

    p = re.compile(r'http://|www\S*', re.IGNORECASE) # retirar links
    msg = p.sub('', msg)
    p = re.compile(r'(\w)\1{2,}', re.IGNORECASE) # retirar mais que 2 letras repetidas
    msg = p.sub(r'\1', msg)
    p = re.compile(r'([!.?])\1{1,}', re.IGNORECASE) # retirar mais que 1 ponto repetido e adicionar um espaço após
    msg = p.sub(r'\1 ', msg) 
    p = re.compile(r' ([!?.])') # retirar um espaço antes de pontuação
    msg = p.sub(r'\1', msg)
    p = re.compile(r'(\w)(ao)\s', re.IGNORECASE) # colocar ~ numa palavra terminada em ao
    msg = p.sub(r'\1ão ', msg)

    for (abr, s_abr) in abreviacao:
        p = re.compile(abr, re.IGNORECASE)
        msg = p.sub(s_abr, msg)

    comentario['corrigido'] = msg
    return comentario
