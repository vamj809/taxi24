# Taxi24
Una RESTful API genérica para la industria del transporte.

Utiliza un ejecutable para iniciar el servidor disponible en: http://localhost:5000 y este lo redirigirá a la dirección segura https://localhost:5001

Puede utilizar Postman para comprobar cada uno de los requests disponibles.

Hay 4 controladores disponibles para cada una de las funcionalidades requeridas:

# Index - Indice #
## GET: / 
Devuelve el listado de los controladores disponibles

# Conductores #

## GET: /api/Conductores
Devuelve el listado de todos los conductores/pilotos.

## GET: api/Conductores/available
Devuelve una lista de todos los conductores disponibles (No se encuentran en medio de un viaje)

## GET: api/Conductores/available-radio
Devuelve una lista de todos los conductores disponibles en un radio de 3km para una ubicación específica.
> **Params:** Latitud _(double)_ y Longitud _(double)_

## GET: api/Conductores/{id}
Devuelve la información de un conductor especifico.
> **Params:** ID _(integer)_

# Pasajeros #

## GET: api/Pasajeros
Devuelve el listado de todos los pasajeros

## GET: api/Pasajeros/{id}
Devuelve la información de un pasajero especifico.
> **Params:** ID _(integer)_

## GET: api/Pasajeros/ride/{id}
En preparación para un viaje, devuelve la información de los tres (3) conductores más cercanos al pasajero con el ID respectivo.
> **Params:** PasajeroID _(integer)_

# Viajes #

## POST: api/Viajes
Crea una nueva solicitud de viaje.
> **Params:** PasajeroID _(integer)_, ConductorID _(integer)_

## PUT: api/Viajes/complete/{id}
Modifica un viaje de forma que se marque como completado.
> **Params:** ViajeID _(integer)_

## GET: api/Viajes/active
Devuelve todos los viajes en curso (no-completados)
