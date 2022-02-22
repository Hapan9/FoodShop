let url = 'https://localhost:44383/api/';

fetch(url + 'Users').then(x => x.json()).then(result => console.log(result));

let form = document.querySelector('.login-register-form form');
form.onsubmit = () => {
  let login = document.querySelector('#password-login').value;
  let password = document.querySelector('#password-password').value;
  let user = {
    name: 'string',
    surname: 'string',
    role: 0,
    login: login,
    password: password
  };

  fetch(url + `Users`, {
    method: 'post',
    body: JSON.stringify(user),
    headers: {
      'Content-Type': 'application/json'
    }
  }).then(() => { });

};