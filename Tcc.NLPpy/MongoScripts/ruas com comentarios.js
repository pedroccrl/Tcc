db.getCollection('rua').find({$where: 'this.comentarios && this.comentarios.length > 0 && this.Nome.length > 6' })