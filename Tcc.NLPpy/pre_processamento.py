# pylint: disable=C0111
# pylint: disable=C0103

pontos = ['.',',','!','?',';']
acentos = ['á','é','í','ó','ú','à','è','ì','ò','ù','ã','ẽ','ĩ','õ','ũ','â','ê','î','ô','û']
s_acentos = ['a','e','i','o','u','a','e','i','o','u','a','e','i','o','u','a','e','i','o','u']

abreviacao = [('vc', 'você'), ('vcs', 'vocês'), ('q', 'que')]

def remove_abrev(palavras):
    words = []
    for p in palavras:
        for (ab, s_ab) in abreviacao:
            if p == ab:
                p = s_ab
            words.append(p)
    return words