export default {
    optionsGetTodoList: {
        stages: [
		    { duration: '30', target: 100 },
            { duration: '1m', target: 150 }, //é usada para definir as etapas (ou estágios) de carga durante um teste de carga.
            { duration: '2m', target: 300 }    // Cada estágio pode ter uma duração e um número de usuários-alvo.
            // A configuração indica um único estágio com duração de 1 minuto ('1m') e um usuário-alvo (target)
                                          // definido como 1. Isso significa que, durante esse estágio, o K6 tentará manter uma carga de 1 usuário por um minuto inteiro.
            // { duration: '1m', target: 2 }, 
            // { duration: '1m', target: 3 },
            // { duration: '1m', target: 0 }
        ],
        thresholds: {
            'http_req_duration': ['p(90)<7000'], //Essa parte da expressão define a assertividade de desempenho para a métrica 'http_req_duration'.
                                                 //Nesse caso, está definido como p(90)<3000, o que significa que a assertividade é 
                                                 //verdadeira se 90% das solicitações tiverem uma duração menor que 7000 milissegundos (ou 7 segundos).
        },
    },
    BASE_URL: 'http://localhost:8080/',
    params: {
        timeout: '120s',
		lowercase: true
    }
}