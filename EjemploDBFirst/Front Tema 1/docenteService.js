function actualizarPersona(){

    let txtNombre = document.getElementsById("txtNombre")
    let txtApelliddo = document.getElementsById("txtApellido")
    let txtEmail = document.getElementsById("txtEmail")
    let txtEdad = document.getElementsById("txtEdad")
    let txtNivel = document.getElementsByName("txtNivel")

     txtEdad = parseInt(edadInput.value);

    if(txtNombre.value === ""){
        alert("El nombre es obligatorio")
        return false;
    }

    if(txtApelliddo.value === ""){
        alert("El nombre es obligatorio")
        return false;
    }

    if(txtEmail.value === ""){
        alert("El nombre es obligatorio")
        return false;
    }

    if (txtEdadedad <= 0 || edad >= 65 || isNaN(edad)) {
        alert('El docente no puede ser jubilado');
        return false;
      }

    if(txtNivel.value === ""){
        alert("El nombre es obligatorio")
        return false;
    }

    const url="https://localhost:7169/api/docentes/actualizarDocentes"
    const request = {
        "id" : 140, 
        "nombre": nombre,
        "apellido": apellido,
        "email": email,
        "edad": edad,
        "nivel": nivel

    }
    fetch(url, {
        body: JSON.stringify(request),
        method: "put",
        headers: {
            "Content-Type": "aplication/json"
        }
    })
    .then(respuesta => respuesta.json())
    .then(respuesta =>{
        if(respuesta.ok){
            alert("Persona modificada con exito")
            localStorage.setItem("datoAMostrar", respuesta.listDocentes[0].nombre)
            
        }
        else{
            alert("Error al encontrar al docente")
        }
    })
    .catch(err => alert("Error: " + err))

}