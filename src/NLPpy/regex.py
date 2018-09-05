# pylint: disable=C0111
# pylint: disable=C0103
import re
import unicodedata
import RemovedorDeAcentos

exampleString = '''Jessica is 15 years old, and Daniel is 27 years old.. amanha eh um novo dia . e hoje tbm ! adeus nao tao  aaaogd
Edward is 97 years old, and his grandfather, Oscar, is 102. acoooordemmm... http://www.globo.com/sadfasdfdSDAS  www.globo.com..
rua claudio ribeiro esta um lixo! rua da jaqueira; rua dos absurodos - cep ashdausdhas GRAÇA
'''
# ages = re.findall(r'\d{1,3}', exampleString)
# names = re.findall(r'\s([A-Z][a-z]*)', exampleString)
# # rep = re.findall(r'((\w)\2{2,})', exampleString)
# rep = re.findall(r'(\w)\1{2,}', exampleString)
# links = re.findall(r'http://|www\S*\s', exampleString)
# pontos = re.findall(r'[.!?]{2,}', exampleString)
# tio = re.findall(r'\w(ao)\s', exampleString)
# words = re.findall(r'(\S)+', exampleString)
# print(words)
# print(tio)
# print(ages)
# print(names)
# print(rep)
# print(pontos)

# p = re.compile(r'http://|www\S*')
# exampleString = p.sub('', exampleString)
# p = re.compile(r'(\w)\1{2,}', re.IGNORECASE)
# exampleString = p.sub(r'\1', exampleString)
# p = re.compile(r'([!.?])\1{1,}')
# exampleString = p.sub(r'\1 ', exampleString)
# p = re.compile(r' ([!?.])')
# exampleString = p.sub(r'\1', exampleString)
# 
# p = re.compile(r'(\w)(ao)\s', re.IGNORECASE)
# exampleString = p.sub(r'\1ão ', exampleString)
# print(exampleString)

# p = re.compile(r'\s(da|do|de|das|dos)\s', re.IGNORECASE)
# p = re.compile(r'-.*', re.IGNORECASE)
p = re.compile(r'graca', re.UNICODE|re.IGNORECASE)
msg = p.sub('gra', RemovedorDeAcentos.remover_acentos(exampleString))
print(msg)