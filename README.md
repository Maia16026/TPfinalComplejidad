# Sistema de Búsqueda por Coincidencias Aproximadas (BK-Tree)

Este módulo implementa una solución técnica para la búsqueda de términos basada en su similitud textual. Utiliza una estructura de **Árbol BK** (Burkhard-Keller) para organizar la información y la **Distancia de Levenshtein** como métrica para determinar la cercanía entre palabras.

## Funcionalidades Técnicas

### 1. Inserción de Datos (`AgregarDato`)
El sistema permite construir el árbol de forma dinámica. Cada dato se inserta comparando la distancia de Levenshtein entre el nuevo término y los nodos existentes.
* **Normalización:** Se aplica `Trim()` y `ToLowerInvariant()` para asegurar que los espacios o mayúsculas no afecten la precisión.
* **Lógica Recursiva:** Utiliza una función interna para descender por la jerarquía hasta encontrar el lugar donde la distancia de la arista coincida con el cálculo de similitud.

*  **Informe del Trabajo Final:**
[InformeTrabajoFinalCTEDACorralMaia.pdf](https://github.com/user-attachments/files/23265058/InformeTrabajoFinalCTEDACorralMaia.pdf)
