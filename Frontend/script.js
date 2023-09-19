document.addEventListener("DOMContentLoaded", search);

const URL_API = "https://localhost:7105/api/";

function init() {
  search();
}

async function search() {
  var URL = URL_API + "customer";

  var response = await fetch(URL, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  var resultado = await response.json();

  console.log(resultado);

  var html = "";

  for (customer of resultado) {
    var row = `<tr>
        <td>${customer.firstName}</td>
        <td>${customer.lastName}</td>
        <td>${customer.email}</td>
        <td>${customer.phone}</td>
        <td>${customer.address}</td>
        <td>
          <button onclick="edit(${customer.id})" id="edictar">Edictar</button>
          <button onclick="remove(${customer.id})" id="eliminar">Eliminar</button>
        </td>
      </tr>`;

    html += row;
  }

  document.querySelector("#customer > tbody").outerHTML = html;


}

function agregarCliente() {
  var htmlModal = document.getElementById("modal");
  htmlModal.setAttribute("class", "modale opened");
}

function cerrarModal() {
  var htmlModal = document.getElementById("modal");
  htmlModal.setAttribute("class", "modale");
}

async function edit(id) {

  
  var URL = URL_API + "customer/" + id;

  var response = await fetch(URL, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  var customer = await response.json();

  console.log(customer);

  agregarCliente();
  
  document.getElementById("txtAddress").value = customer.address;
  document.getElementById("txtFirstname").value = customer.firstName;
  document.getElementById("txtLastname").value = customer.lastName;
  document.getElementById("txtEmail").value = customer.email;
  document.getElementById("txtPhone").value = customer.phone;
  document.getElementById("txtId").value = customer.id;


  document.getElementById("btnSave").style.display = "none";
  document.getElementById("btnUpdate").style.display = "block";
}

function limpiar() {
  
  agregarCliente();
  clear();
}

function clear() {
  document.getElementById("txtAddress").value ="";
  document.getElementById("txtFirstname").value ="";
  document.getElementById("txtLastname").value ="";
  document.getElementById("txtEmail").value ="";
  document.getElementById("txtPhone").value ="";
  document.getElementById("txtId").value ="";

}

async function save() {

  var id = document.getElementById("txtId").value;;

  var URL = URL_API + "customer";

  var customer = {
    "address": document.getElementById("txtAddress").value,
    "firstName": document.getElementById("txtFirstname").value,
    "lastName": document.getElementById("txtLastname").value,
    "email": document.getElementById("txtEmail").value,
    "phone": document.getElementById("txtPhone").value,
    
  }

  if (id != "") {
    customer.id = id;
  }

  await fetch(URL, {
    "method": id!=""? "PUT" : "POST",
    "body": JSON.stringify(customer),
    "headers": {
      "Content-Type": "application/json",
    },
  });


  window.location.reload();
}

async function remove(id) {
  var respuesta = confirm("Desea eliminar el registro?");
  if (respuesta) {
    var URL = URL_API + "customer/" + id;

    await fetch(URL, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    });

    
  }

  window.location.reload();

}
