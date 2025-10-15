//              deux variables globales
let parent = document.getElementsByTagName('main');
var id = "";


//              affichage de la partie accueil
let choix = document.createElement('div')
        choix.className = `Start`;
        choix.innerHTML=`<button class="but1">Tour par tour</button>
                        <button class="but2">Globalité</button>`
        parent[0].appendChild(choix);

let but = document.getElementsByTagName('button');

but[0].addEventListener("click", event => {
        let BalFlux = document.getElementById("flux");
        id = BalFlux.value;
      });

but[1].addEventListener("click", event => {
  let start = document.getElementsByClassName('Start');
  start[0].remove();
  ButTPT();
});

but[2].addEventListener("click", event => {
  let start = document.getElementsByClassName('Start');
  start[0].remove();
  ButGlob();
});


//              variables globales utilisées pour toutes les fonctions en rapport avec l'affichage tour par tour
var roundIndex = 1;
let VisGlob = document.createElement('div')
let TourCour = document.createElement('section')
let Dtest = document.createElement('div')

//              bouton servant à sélectionner le flux JSON
function ButTPT() {
        but[0].addEventListener("click", event => {
                        let BalFlux = document.getElementById("flux");
                        id = BalFlux.value;
                        event.preventDefault();
                        TourParTour();
                      });
                }

//              fonction qui sert à mettre tous les éléments HTML dont l'affichage tour par tour a besoin dans le main
function TourParTour() {

        fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/players`)
        .then(response => response.json())
        .then(dataPla => {
                  Players = dataPla;
        })

    let TPT = document.createElement('div')
            TPT.className = `TourParTour`;
            parent[0].appendChild(TPT);
    TPT = document.getElementsByClassName('TourParTour')

            VisGlob.className = `vision`;
            TPT[0].appendChild(VisGlob);
    VisGlob = document.getElementsByClassName('vision')

    let TourPres = document.createElement('aside')
            TourPres.className = `AsTPT`;
            TourPres.innerHTML = `<button class="TourP">⬅ Tour précédent</button>`
            VisGlob[0].appendChild(TourPres);

            TourCour.className = `SecTPT`;
            VisGlob[0].appendChild(TourCour);


            TourCour.appendChild(Dtest);


    // Affiche roundIndex = 1
    fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/rounds/${roundIndex}`)
        .then(response => response.json())
        .then(dataRoun => {
                Dtest.innerHTML = `<div class="Div1">
                                        <h2>Tour en cours : numéro ${dataRoun.id}</h2>
                                        <h4>Joueur : ${Players[0].pseudo}</h4>
                                        <p><br></p>
                                        <ul>
                                          <li>Les dés obtenus : ${dataRoun.results[0].dice[0]} ${dataRoun.results[0].dice[1]} ${dataRoun.results[0].dice[2]} ${dataRoun.results[0].dice[3]} ${dataRoun.results[0].dice[4]}</li>
                                          <li>Le challenge choisi : ${dataRoun.results[0].challenge}</li>
                                          <li>Le nombre de points gagnés : ${dataRoun.results[0].score}</li>
                                         </ul>
                                      </div>

                                      <div class="Div2">
                                        <h2>Tour en cours : numéro ${dataRoun.id}</h2>
                                        <h4>Joueur : ${Players[1].pseudo}</h4>
                                         <p><br></p>
                                        <ul>
                                          <li>Les dés obtenus : ${dataRoun.results[1].dice[0]} ${dataRoun.results[1].dice[1]} ${dataRoun.results[1].dice[2]} ${dataRoun.results[1].dice[3]} ${dataRoun.results[1].dice[4]}</li>
                                          <li>Le challenge choisi : ${dataRoun.results[1].challenge}</li>
                                          <li>Le nombre de points gagnés : ${dataRoun.results[1].score}</li>
                                         </ul>
                                      </div>`
                TourCour.appendChild(Dtest);
    })


    let TourSuiv = document.createElement('aside')
            TourSuiv.className = `AsTPT`;
            TourSuiv.innerHTML = `<button class="TourS">Tour suivant ➡</button>`
            VisGlob[0].appendChild(TourSuiv);

    let ScoreFin = document.createElement('footer')
            ScoreFin.className = `FoTPT`;
            TPT[0].appendChild(ScoreFin);
    Score();
    // paramétrage des bouttons        
    Bout();
    
}


//              fonction servant à remplir le footer
function Score() {

        var gagnant;
        let affSco = document.getElementsByClassName('FoTPT')

  

  fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/final-result`)

      .then(response => response.json())
      .then(dataFR => {
                let j1 = document.createElement('section')
                        j1.innerHTML=`<h2>${Players[0].pseudo}</h2>
                                <p>bonus : ${dataFR[0].bonus}</p>
                                <p>score : ${dataFR[0].score}</p>`;
                        affSco[0].appendChild(j1);

                let j2 = document.createElement('section')
                        j2.innerHTML=`<h2>${Players[1].pseudo}</h2>
                                <p>bonus : ${dataFR[1].bonus}</p>
                                <p>score : ${dataFR[1].score}</p>`;
                        affSco[0].appendChild(j2);

                if (dataFR[0].score < dataFR[1].score)
                {
                gagnant = Players[1].pseudo;
                }
                else if (dataFR[0].score > dataFR[1].score)
                {
                gagnant = Players[0].pseudo;
                }
                else
                {
                gagnant = "Les deux joueurs sont à égalité";
                }

                let Final = document.createElement('section')
                Final.className = `SecGagn1`;
                Final.innerHTML=`<h2>Résultat final</h2>
                                <p>Le gagnant est : ${gagnant}</p>`;
                affSco[0].appendChild(Final);
      })
}


//              fonction servant pour les boutons des asides
function Bout() {

        let but = document.getElementsByTagName('button');
        but[1].addEventListener("click", event => {
                if (roundIndex > 1)
                {
                        roundIndex--;
                        ModifSec();
                        //envoie vers une fonction qui modifie la section en fonction du roundIndex
                }
        });
                
        but[2].addEventListener("click", event => {
                if (roundIndex < 13)
                {
                        roundIndex++;
                        ModifSec();
                        //envoie vers une fonction qui modifie la section en fonction du roundIndex
                }
        });
}



//              fonction qui modifie la section en fonction du roundInxex
function ModifSec() {
        fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/rounds/${roundIndex}`)
        .then(response => response.json())
        .then(dataRoun => {
                Dtest.innerHTML = `<div class="Div1">
                                        <h2>Tour en cours : numéro ${dataRoun.id}</h2>
                                        <h4>Joueur : ${Players[0].pseudo}</h4>
                                         <p><br></p>
                                        <ul>
                                          <li>Les dés obtenus : ${dataRoun.results[0].dice[0]} ${dataRoun.results[0].dice[1]} ${dataRoun.results[0].dice[2]} ${dataRoun.results[0].dice[3]} ${dataRoun.results[0].dice[4]}</li>
                                          <li>Le challenge choisi : ${dataRoun.results[0].challenge}</li>
                                          <li>Le nombre de points gagnés : ${dataRoun.results[0].score}</li>
                                         </ul>
                                      </div>

                                      <div class="Div2">
                                        <h2>Tour en cours : numéro ${dataRoun.id}</h2>
                                        <h4>Joueur : ${Players[1].pseudo}</h4>
                                         <p><br></p>
                                        <ul>
                                          <li>Les dés obtenus : ${dataRoun.results[1].dice[0]} ${dataRoun.results[1].dice[1]} ${dataRoun.results[1].dice[2]} ${dataRoun.results[1].dice[3]} ${dataRoun.results[1].dice[4]}</li>
                                          <li>Le challenge choisi : ${dataRoun.results[1].challenge}</li>
                                          <li>Le nombre de points gagnés : ${dataRoun.results[1].score}</li>
                                         </ul>
                                      </div>`
                TourCour.appendChild(Dtest);
        })
        // retourne ensuite dans la fonction Bout
}

//              bouton servant à sélectionner le flux JSON
function ButGlob() {
but[0].addEventListener("click", event => {
                let BalFlux = document.getElementById("flux");
                id = BalFlux.value;
                event.preventDefault();
                Globalite();
              });
        }

//              fonction servant à afficher l'affichage global
function Globalite() {   

  let Gene = document.createElement('div')
          Gene.className = `Globalite`;
          parent[0].appendChild(Gene);

  let par = document.getElementsByClassName('Globalite');

  var Players;
  var gagnant;

  fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/players`)
      .then(response => response.json())
      .then(dataPla => {
                Players = dataPla;
      })

  fetch(`http://yams.iutrs.unistra.fr:3000/api/games/${id}/final-result`)

      .then(response => response.json())
      .then(dataFR => {
            let j1 = document.createElement('section')
                    j1.innerHTML=`<h2>${Players[0].pseudo}</h2>
                                  <p><br>bonus : ${dataFR[0].bonus}</p>
                                  <p>score : ${dataFR[0].score}</p>`;
                    par[0].appendChild(j1);

            let j2 = document.createElement('section')
                    j2.innerHTML=`<h2>${Players[1].pseudo}</h2>
                                  <p><br>bonus : ${dataFR[1].bonus}</p>
                                  <p>score : ${dataFR[1].score}</p>`;
                    par[0].appendChild(j2);
            
            if (dataFR[0].score < dataFR[1].score)
            {
              gagnant = Players[1].pseudo;
            }
            else if (dataFR[0].score > dataFR[1].score)
            {
              gagnant = Players[0].pseudo;
            }
            else
            {
              gagnant = "Les deux joueurs sont à égalité";
            }

            let Final = document.createElement('section')
          Final.className = `SecGagn`;
          Final.innerHTML=`<h2>Résultat final</h2>
                            <p><br>Le gagnant est : ${gagnant}</p>`;
          parent[0].appendChild(Final);        
  })
}