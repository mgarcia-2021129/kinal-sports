/*console.log("Inicio");

setTimeout(() =>{
    console.log("Saludo despues de 2 segundos")
}, 2000)

console.log("Fin")

node index.js


const obtenerDatos = (callback) => {
    setTimeout(() =>{
        const datos = "Datos obteniudos de la db"
        callback(datos)
    }, 3000)
}

obtenerDatos((datos) =>{
    console.log(datos)
})

console.log("Inicio Callback")*/

//PROMESAS
/*const inicioSecion = (id) =>{
    return new Promise((resolve, reject) => {
        setTime(() =>{
            if( id === 1){
                resolve({
                    id: 1,
                    nombre: "Ricardo Hernández",
                    edad: 18
                })
            }else{
                reject("El usuario no existe")
            }
        }, 2000)
    })
}

inicioSecion(1)
    .then((respuesta) => console.log(respuesta))
    .catch((error) => console.log(error))

const manejarInicioSesion = async () =>{
    try {
        const respuesta = await inicioSecion(1)
        console.log(respuesta)
    } catch (error) {
        console.log(error)
    }
}

manejarInicioSesion();*/

//OPERADORES AVANZADOS 

const edad = 18
if(edad >= 18){
    console.log("Es mayor de edad")
}else{
    console.log("Es menor de edad")
}

edado >= 18 
    ? console.log("Es mayor de edad")
    : console.log("Es menor de edad")

//OPERADOR DE PROPAGACION O SPREAD

const a = [1,2,3]
const b = a
const c = [...a]
a.push(4)

console.log("a:", a)
console.log("b:", b)
console.log("c:", c)