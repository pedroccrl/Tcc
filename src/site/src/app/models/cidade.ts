export interface Cidade {
    _id:        string;
    nome:       string;
    idCidade:   number;
    temas:      Array<Array<number | string>>;
    qualidades: Array<Array<number | string>>;
}