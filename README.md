# ClimaxAI.API

API desarrollada en .NET 8 para análisis inteligente del impacto climático por sector económico, integrando datos meteorológicos en tiempo real y análisis mediante Inteligencia Artificial.

---

## 🚀 Descripción del Proyecto

ClimaxAI es una API REST que:

- Consulta datos climáticos en tiempo real desde Open-Meteo.
- Calcula un índice de impacto climático (0–100).
- Clasifica el nivel de riesgo: Bajo, Medio o Alto.
- Genera recomendaciones estratégicas por sector económico.
- Integra análisis ejecutivo automático usando IA (OpenRouter).
- Guarda historial de consultas en memoria.
---

## 🏗 Arquitectura del Proyecto

El proyecto está organizado en capas:

- **Controllers** → Exponen los endpoints REST.
- **Services** → Contienen la lógica de negocio y consumo de APIs externas.
- **Models** → DTOs y estructuras de datos.
- **Helpers** → Funciones auxiliares para interpretación de datos climáticos.

---

## 🧠 Flujo de Funcionamiento

1. El usuario consulta `/api/Test/clima`.
2. La API consume datos desde Open-Meteo.
3. Se calcula el índice de impacto según el sector.
4. Se determina el nivel de riesgo.
5. Se genera una recomendación estratégica.
6. Se envía la información a la IA.
7. Se retorna respuesta completa al cliente.
8. Se guarda el registro en historial en memoria.

   ---

## 📡 Endpoints Disponibles

### 🔎 Consultar Clima por Sector

**GET**

/api/Test/clima?lat=4.7110&lon=-74.0721&sector=agricultura


### Parámetros

| Parámetro | Tipo   | Descripción |
|-----------|--------|------------|
| lat       | double | Latitud (-90 a 90) |
| lon       | double | Longitud (-180 a 180) |
| sector    | string | Sector económico (agricultura, logistica, construccion) |

### Respuesta Ejemplo

```json
{
  "temperatura": 18.4,
  "velocidadViento": 12.3,
  "descripcion": "Parcialmente nublado",
  "indiceImpacto": 72,
  "nivelRiesgo": "Alto",
  "recomendacion": "Alto riesgo de afectación en cultivos.",
  "analisisIA": "Impacto operativo moderado generado por IA."
}
```
---

### 📜 Consultar Historial de Consultas

**GET**

/api/Test/historial

### Descripción

Devuelve la lista de todas las consultas climáticas realizadas durante la ejecución de la API.

### Respuesta Ejemplo

```json
[
  {
    "temperatura": 18.4,
    "velocidadViento": 12.3,
    "descripcion": "Parcialmente nublado",
    "indiceImpacto": 72,
    "nivelRiesgo": "Alto",
    "recomendacion": "Alto riesgo de afectación en cultivos.",
    "analisisIA": "Impacto operativo moderado generado por IA."
  }
]
```






