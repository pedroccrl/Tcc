export interface Comentario {
    _id:            string;
    created_time:   string;
    from:           From;
    message:        string;
    like_count:     number;
    comments:       null;
    idComentario:   number;
    idRespondido:   null;
    idCidade:       number;
    idPagina:       number;
    idPost:         number;
    corrigido:      string;
    tema:           Tema;
    temLogradouro:  boolean;
    logradouros:    Logradouro[];
    nome:           null;
}

export interface From {
    _id:  string;
    name: string;
}

export interface Logradouro {
    idBairro:  number;
    nome:      string;
    cep:       string;
    longitude: number | null;
    _id:       number;
    latitude:  number | null;
    bairro:    Bairro;
    idCidade:  number;
}

export interface Bairro {
    nomeAlternativo: string;
    nome:            string;
}

export interface Tema {
    nomes:      string[];
    qualidades: string[];
    pos_tags:   Array<Array<string[]>>;
}
