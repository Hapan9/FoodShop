let url = 'https://localhost:44383/api/';

readAllItems();
readAllCategories();

async function readAllItems() {
  await fetch(url + 'Products')
    .then(result => result.json())
    .then(result => {
      let parent = document.querySelector('.col-2-3 .items');
      for (let item of result) {
        let element = document.createElement('div');
        parent.append(element);
        element.classList.add('shop-item');
        let img = document.createElement('img');
        element.append(img);
        img.setAttribute('src', item.photo);
        let h2 = document.createElement('h2');
        element.append(h2);
        let h2a = document.createElement('a');
        h2.append(h2a);
        h2a.setAttribute('href', `./item-details.html?id=${item.id}`);
        h2a.textContent = item.name;
        let h4 = document.createElement('h4');
        element.append(h4);
        h4.textContent = item.description;
        let small = document.createElement('small');
        element.append(small);
        small.textContent = item.price + ' UAH';
      }
    });
}

async function readAllCategories() {
  await fetch(url + 'ProductsInfo/Names')
    .then(result => result.json())
    .then(result => {
      let parent = document.querySelector('.col-1-3 .categories ul');
      for (let item of result) {
        let element = document.createElement('li');
        parent.append(element);
        element.textContent = item;
        element.addEventListener('click', () => {
          readItemsByCategory(item);
        });
      }
    });
}

async function readItemsByCategory(category) {
  let parent = document.querySelector('.col-2-3 .items');
  parent.innerHTML = "";
  await fetch(url + `ProductsInfo/${category}/Products`)
    .then(result => result.json())
    .then(result => {
      for (let id of result) {
        fetch(url + `Products/${id}`).then(x => x.json()).then(item => {
          let element = document.createElement('div');
          parent.append(element);
          element.classList.add('shop-item');
          let img = document.createElement('img');
          element.append(img);
          img.setAttribute('src', item.photo);
          let h2 = document.createElement('h2');
          element.append(h2);
          let h2a = document.createElement('a');
          h2.append(h2a);
          h2a.setAttribute('href', `./item-details.html?id=${item.id}`);
          h2a.textContent = item.name;
          let h4 = document.createElement('h4');
          element.append(h4);
          h4.textContent = item.description;
          let small = document.createElement('small');
          element.append(small);
          small.textContent = item.price + ' UAH';
        });
      }
    });
}



