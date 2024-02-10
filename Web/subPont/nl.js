"use strict"
const tomb = [
  {
    "OM_Azonosito": "78655218932",
    "Neve": "Szabó Anna",
    "ErtesitesiCime": "Budapest, Gellért tér 15.",
    "Email": "anna@example.com",
    "SzuletesiDatum": "1998-07-19T00:00:00",
    "Matematika": 14,
    "Magyar": 35
  },
  {
    "OM_Azonosito": "15963702584",
    "Neve": "Nagy Zsófia",
    "ErtesitesiCime": "Debrecen, Szent István utca 8.",
    "Email": "zsofia@example.com",
    "SzuletesiDatum": "2000-02-22T00:00:00",
    "Matematika": 27,
    "Magyar": 4
  },
  {
    "OM_Azonosito": "30351479261",
    "Neve": "Kovács Máté",
    "ErtesitesiCime": "Szeged, Erzsébet körút 45.",
    "Email": "mate@example.com",
    "SzuletesiDatum": "1995-11-29T00:00:00",
    "Matematika": 48,
    "Magyar": 15
  },
  {
    "OM_Azonosito": "97401028543",
    "Neve": "Tóth Bence",
    "ErtesitesiCime": "Pécs, Váci utca 33.",
    "Email": "bence@example.com",
    "SzuletesiDatum": "1997-03-17T00:00:00",
    "Matematika": 8,
    "Magyar": 47
  },
  {
    "OM_Azonosito": "88765031624",
    "Neve": "Horváth Eszter",
    "ErtesitesiCime": "Székesfehérvár, Bartók Béla út 12.",
    "Email": "eszter@example.com",
    "SzuletesiDatum": "1996-09-08T00:00:00",
    "Matematika": 34,
    "Magyar": 7
  },
  {
    "OM_Azonosito": "64189075351",
    "Neve": "Kiss Attila",
    "ErtesitesiCime": "Miskolc, József Attila utca 18.",
    "Email": "attila@example.com",
    "SzuletesiDatum": "1993-12-05T00:00:00",
    "Matematika": 13,
    "Magyar": 48
  },
  {
    "OM_Azonosito": "18734250658",
    "Neve": "Fekete Laura",
    "ErtesitesiCime": "Győr, Széchenyi tér 9.",
    "Email": "laura@example.com",
    "SzuletesiDatum": "1999-06-30T00:00:00",
    "Matematika": 2,
    "Magyar": 30
  },
  {
    "OM_Azonosito": "51698072427",
    "Neve": "Bíró Gábor",
    "ErtesitesiCime": "Kecskemét, Deák Ferenc utca 21.",
    "Email": "gabor@example.com",
    "SzuletesiDatum": "1994-10-14T00:00:00",
    "Matematika": 9,
    "Magyar": 33
  },
  {
    "OM_Azonosito": "60157349268",
    "Neve": "Mészáros Péter",
    "ErtesitesiCime": "Nyíregyháza, Petőfi Sándor utca 26.",
    "Email": "peter@example.com",
    "SzuletesiDatum": "2001-04-01T00:00:00",
    "Matematika": 36,
    "Magyar": 21
  },
  {
    "OM_Azonosito": "72948316750",
    "Neve": "Varga Noémi",
    "ErtesitesiCime": "Szombathely, Kossuth Lajos utca 3.",
    "Email": "noemi@example.com",
    "SzuletesiDatum": "1992-08-18T00:00:00",
    "Matematika": 24,
    "Magyar": 23
  },
  {
    "OM_Azonosito": "84052731649",
    "Neve": "Lakatos Dóra",
    "ErtesitesiCime": "Veszprém, Ady Endre utca 7.",
    "Email": "dora@example.com",
    "SzuletesiDatum": "2000-01-03T00:00:00",
    "Matematika": 43,
    "Magyar": 41
  },
  {
    "OM_Azonosito": "85273941680",
    "Neve": "Németh Tamás",
    "ErtesitesiCime": "Szolnok, Béke tér 14.",
    "Email": "tamas@example.com",
    "SzuletesiDatum": "1998-05-27T00:00:00",
    "Matematika": 5,
    "Magyar": 49
  },
  {
    "OM_Azonosito": "41593260701",
    "Neve": "Orbán Katalin",
    "ErtesitesiCime": "Eger, Szabadság utca 32.",
    "Email": "katalin@example.com",
    "SzuletesiDatum": "1996-02-11T00:00:00",
    "Matematika": 37,
    "Magyar": 20
  },
  {
    "OM_Azonosito": "10486732952",
    "Neve": "Simon Balázs",
    "ErtesitesiCime": "Debrecen, Király utca 28.",
    "Email": "balazs@example.com",
    "SzuletesiDatum": "1995-07-07T00:00:00",
    "Matematika": 20,
    "Magyar": 48
  },
  {
    "OM_Azonosito": "92740581643",
    "Neve": "Papp Viktória",
    "ErtesitesiCime": "Kaposvár, Alkotmány utca 5.",
    "Email": "viktor@example.com",
    "SzuletesiDatum": "1997-11-24T00:00:00",
    "Matematika": 32,
    "Magyar": 9
  },
  {
    "OM_Azonosito": "10637851454",
    "Neve": "Molnár Zoltán",
    "ErtesitesiCime": "Szekszárd, Párizsi utca 17.",
    "Email": "zoltan@example.com",
    "SzuletesiDatum": "1993-01-16T00:00:00",
    "Matematika": 3,
    "Magyar": 46
  },
  {
    "OM_Azonosito": "44025967885",
    "Neve": "Fekete Márton",
    "ErtesitesiCime": "Pécs, Rákóczi út 13.",
    "Email": "marton@example.com",
    "SzuletesiDatum": "1992-04-29T00:00:00",
    "Matematika": 42,
    "Magyar": 31
  },
  {
    "OM_Azonosito": "30381425616",
    "Neve": "Pál Júlia",
    "ErtesitesiCime": "Sopron, Szent Gellért tér 10.",
    "Email": "julia@example.com",
    "SzuletesiDatum": "1999-09-02T00:00:00",
    "Matematika": 49,
    "Magyar": 19
  },
  {
    "OM_Azonosito": "65082317920",
    "Neve": "Takács Orsolya",
    "ErtesitesiCime": "Eger, Andrássy út 22.",
    "Email": "orsolya@example.com",
    "SzuletesiDatum": "1994-06-13T00:00:00",
    "Matematika": 31,
    "Magyar": 18
  },
  {
    "OM_Azonosito": "15374680221",
    "Neve": "Kovács Ádám",
    "ErtesitesiCime": "Székesfehérvár, Bajnai út 8.",
    "Email": "adam@example.com",
    "SzuletesiDatum": "1996-08-06T00:00:00",
    "Matematika": 18,
    "Magyar": 10
  }
];

// Legenerálja a táblázat sorait, a tömb objektumbból
function generateTableRow(tomb) {
  const row = document.createElement('tr');

  // Definiálja a cellákat amik meg lesznek jelenítve a táblázatban
  const cells = [
    { key: 'OM_Azonosito', label: 'OM Azonositó' },
    { key: 'Neve', label: 'Név' },
    { key: 'Matematika', label: 'Matematika' },
    { key: 'Magyar', label: 'Magyar' },
    { key: 'sum', label: 'Összesen' }
  ];

  // Végig megy a "cells" tömbön, ahol ellenőrzi a kulcsokat, amennyiben a kulcs "sum" akkor összeadja a "Matematika" és a "Maggyar 
  // mezőben található értéket és ezt beleteszi a "sum"-al elátott mező propertyjébe, a többi esetben meg csak hozzárendeli az értéket az ahhoz tartozó kulcshoz.
  
  cells.forEach(cell => {
    const cellContent = cell.key === 'sum'
      ? tomb.Matematika + tomb.Magyar
      : tomb[cell.key];

      // Létrehozza a "td" elementet
    const cellElement = document.createElement('td');
    // Beleteszi a td elementbe a megfelelő értéket
    cellElement.textContent = cellContent;
    // Beleteszi a "td" elementet a mefelelő sorba
    row.appendChild(cellElement);
  });

  return row;
}

// Kezeli regex segítésgével, hogy a paramétreben kapott érték szám-e és hogy ha igen akkor 100 és 0 között van-e 
function NumberValidation(input) {
  const regex = /^\d+$/; 
  const number = parseInt(input, 10);
  return regex.test(input) && number >= 0 && number <= 100;
}

// Rendezi és szortírozza az adatot a megadott kritérium szerint
function dataSort() {
  const sumValue = parseInt(inpPontszam.value, 10);

  // Ha a pontszám mező üres, és megnyomják a gombot, akkor töltse be az eredeti listát, és jelenítse meg azt 'ABC' sorrendben.
  if (document.getElementById("inpPontszam").value == "") {
    const sortedData = tomb.sort((a, b) => a.Neve.localeCompare(b.Neve));

    // Kiüríti a jelenlegi elemeket a táblázatból, azzal, hogy az innerHTML-t üresre teszi
    const tableBody = document.getElementById('tableBody');
    tableBody.innerHTML = '';

    // Végig megy a szortírozott listán és a generateTableRow() function-t meghívva újra generálja a táblázatot
    sortedData.forEach(item => {
      const row = generateTableRow(item);
      tableBody.appendChild(row);
    });
  }
  // Ha a megadott input egy szám és a határokon belül van, akkor pontszám alapján 'ABC' sorrendbe szortírozva megjeleníti a neveket.
  else if (NumberValidation(inpPontszam.value)) {
    const filteredData = sumValue ? tomb.filter(item => item.Matematika + item.Magyar > sumValue) : tomb;

    // Nevek szerint sorba rendezi
    const sortedData = filteredData.sort((a, b) => a.Neve.localeCompare(b.Neve));

    // Kiüríti a jelenlegi elemeket a táblázatból, azzal, hogy az innerHTML-t üresre teszi
    const tableBody = document.getElementById('tableBody');
    tableBody.innerHTML = '';
    
    // Végig megy a szortírozott listán és a generateTableRow() function-t meghívva újra generálja a táblázatot
    sortedData.forEach(item => {
      const row = generateTableRow(item);
      tableBody.appendChild(row);
    });
  }
  // Ha az input mező nem üres és/vagy nem számot vagy nem megfelelő számot tartalmaz, akkor ezt egy alert-tel jelzem a felhasználó felé.
  else {
    alert('Kérlek adj meg egy számot 0 és 100 között!');
    inpPontszam.value = "";
  }
}

function CSVExport() {
  // Kiszedi az adatokat a táblázatból
  const tableBody = document.getElementById('tableBody');
  // A kiszedett adatokat egy új tömbbe szervezi, majd a slice segítségével létrehoz egy másolatot, így 
  // a változtatások nem vonatkoznak majd az eredeti adatokra, a ".map" segítségével miden diákot egy külön tömbbe
  // rakunk, majd leszedjük róluk a felesleges whitespace karaktereket, végül a filterrel kiszűrjük az esetleges üres sorokat.
  const displayedData = Array.from(tableBody.rows).slice(0).map(row => {
    const cells = row.cells;
    return [
      cells[0].textContent.trim(),
      cells[1].textContent.trim(),
      cells[2].textContent.trim(),
      cells[3].textContent.trim(),
      cells[4].textContent.trim()
    ];
  }).filter(row => row.length > 0);

  // A kiszedett adatokat átalakítjuk CSV formátumra a string.join segítségével majd sortöréseket rakunk be.
  const csvData = displayedData.map(row => row.join(';')).join('\n');

  // A kész CSV sorokat beletesszük a textarea-ba.
  document.getElementById('txtAreCSV').value = csvData;
}

// Meghívom külön a dataSort fucntion-t hogy betöltéskor is 'ABC' sorrendbe töltse be a neveket.
dataSort();
