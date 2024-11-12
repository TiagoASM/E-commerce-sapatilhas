function setLanguage(lang) {
    let greeting;

    // Dicionário de traduções
    const translations = {
        PT: {
            nav_logo: "Portes Gratuitos",
            saldos_text: "SALDOS ATÉ 50%",
            saldosLink: 'SALDOS',
            mais_vendidos_text: 'Mais Vendidos',
            novidades: 'Novidades',
            verMais: 'Ver Mais →'

        },
        UK: {
            nav_logo: "Free Shipping",
            saldos_text: "SALES UP TO 50%",
            saldosLink: 'SALES',
            mais_vendidos_text: 'Best Sellers',
            novidades_text: 'New Arrivals',
            verMais: 'View More →',

        },
        FR: {
            nav_logo: "Livraison gratuite",
            saldos_text: "SOLDES JUSQU’À 50 %",
            saldosLink: "SOLDE",
            mais_vendidos_text: 'Meilleures Ventes',
            novidades_text: 'Nouveautés',
            verMais: 'Voir Plus →',

        },
        ES: {
            nav_logo: "Envío gratis",
            saldos_text: "REBAJAS DE HASTA 50%",
            mais_vendidos_text: 'Más Vendidos',
            novidades_text: 'Novedades',
            verMais: 'Ver Más →',
            saldosLink: 'SALDOS' // Adicionado para o link de Saldos

        }
    };

    // Altera os textos na página de acordo com a linguagem selecionada
    document.getElementById('nav_logo').innerText = translations[lang].nav_logo;
    document.getElementById('saldos_text').innerText = translations[lang].saldos_text;
    document.getElementById('saldosLink').innerText = translations[lang].saldos;
    document.getElementById('mais_vendidos_text').innerText = translations[lang].mais_vendidos_text
    document.getElementById('novidades_text').innerText = translations[lang].novidades_text;
    document.getElementById('verMais').innerText = translations[lang].verMais;


    // Alerta com a linguagem selecionada
    switch (lang) {
        case 'PT':
            greeting = 'Idioma selecionado: Português';
            break;
        case 'UK':
            greeting = 'Selected language: English';
            break;
        case 'FR':
            greeting = 'Langue sélectionnée: Français';
            break;
        case 'ES':
            greeting = 'Idioma seleccionado: Español';
            break;
        default:
            greeting = 'Idioma não reconhecido';
    }

    alert(greeting);
}