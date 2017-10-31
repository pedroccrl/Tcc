# pylint: disable=C0111
# pylint: disable=C0103
import re
import googlemaps
import mongo

gmaps = googlemaps.Client(key='AIzaSyDERd7y7_S4PFWtxntd3YOtBHq1jOB_pM0')

logradouros_col = mongo.db['logradouros']
ruas_col = mongo.db['rua']

logradouros = [rua for rua in logradouros_col.find({'$where': 'this.Latitude == null'})]
ruas = [rua for rua in ruas_col.find({'$where': 'this.Latitude'})]

def geocodar_google():
    i = 0
    for r in logradouros:
        i += 1
        print('{0}/{1}'.format(i, len(logradouros)), end='\b')

        busca = '{0}'.format(r['Cep'])
        coord = gmaps.geocode(busca)

        if len(coord) > 0:
            loc = coord[0]['geometry']['location']
            r['Latitude'] = loc['lat']
            r['Longitude'] = loc['lng']
            logradouros_col.update_one({'_id':r['_id']}, {'$set':r})

def geocodar_mongo():
    for r in ruas:
        for l in logradouros:
            if r['_id'] == l['_id']:
                logradouros_col.update_one({'_id':r['_id']}, {'$set': {'Latitude': r['Latitude'], 'Longitude': r['Longitude']} })

geocodar_google()
