let url = 'https://localhost:5001/api/';

let documentUrl = document.URL;
let itemId = documentUrl.split('?')[1].split('=')[1];

fetch(url + `Products/${itemId}`)
  .then(x => x.json())
  .then(item => {
    let element = document.querySelector('.item-details');
    let img = document.createElement('img');
    element.append(img);
    img.setAttribute('src', item.photo);
    let h1 = document.createElement('h1');
    element.append(h1);
    h1.textContent = item.name;
    let h3 = document.createElement('h3');
    element.append(h3);
    h3.textContent = item.description;
    let small = document.createElement('small');
    element.append(small);
    small.textContent = item.price + ' UAH';
    let button = document.createElement('button');
    element.append(button);
    button.textContent = 'BUY';
    button.classList.add('btn');
    button.onclick = () => window.close();
  });