
const atletak = [
    { helyezes: 9, eredmeny: 84.19, sportolo: "Adrián Annus", orszagkod: "HUN", helyszin: "Szombathely", datum: "2003.08.10" },
    { helyezes: 11, eredmeny: 83.68, sportolo: "Tibor Gécsek", orszagkod: "HUN", helyszin: "Zalaegerszeg", datum: "1998.09.19" }
  ];
  
  // Listaelem kattintás kezelése
  document.querySelectorAll(".head-card-text-title").forEach((elem) => {
    elem.addEventListener("click", () => {
      const azonosito = elem.id;
      const atledata = atletak.find((a) => a.sportolo.toLowerCase().includes(azonosito.toLowerCase()));
      if (atledata) {
        megjelenitReszletesOldal(atledata);
      }
    });
  });
  
  // Részletes oldal megjelenítése
  function megjelenitReszletesOldal(adatok) {
    const oldalTartalom = `
      <div>
        <h1>${adatok.sportolo} (${adatok.orszagkod})</h1>
        <p><strong>Eredmény:</strong> ${adatok.eredmeny}m</p>
        <p><strong>Helyszín:</strong> ${adatok.helyszin}</p>
        <p><strong>Dátum:</strong> ${adatok.datum}</p>
        <canvas id="eredmenygrafikon" width="400" height="200"></canvas>
        <button onclick="visszaFooldalra()">Vissza</button>
      </div>
    `;
  
    document.body.innerHTML = oldalTartalom;
    rajzolGrafikon(adatok);
  }
  
  // Grafikon rajzolása
  function rajzolGrafikon(adatok) {
    const ctx = document.getElementById("eredmenygrafikon").getContext("2d");
    new Chart(ctx, {
      type: "bar",
      data: {
        labels: ["Eredmény"],
        datasets: [
          {
            label: "Dobási eredmények (m)",
            data: [adatok.eredmeny],
            backgroundColor: "rgba(75, 192, 192, 0.2)",
            borderColor: "rgba(75, 192, 192, 1)",
            borderWidth: 1
          }
        ]
      },
      options: {
        scales: {
          y: { beginAtZero: true }
        }
      }
    });
  }
  
  // Visszatérés a főoldalra
  function visszaFooldalra() {
    location.reload();
  }

  