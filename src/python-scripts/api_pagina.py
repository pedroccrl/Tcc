import requests

host = 'http://127.0.0.1:5000/api/facebook/pagina'

def set_host(h):
    host = h

def post(cidade):
    r = requests.post(host, json=cidade)
    return r

