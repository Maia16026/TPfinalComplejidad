
using System;
using System.Collections.Generic;
using tp1;

namespace tpfinal
{

    public class Estrategia
    {
        private int CalcularDistancia(string str1, string str2)
        {
            // using the method
            String[] strlist1 = str1.ToLower().Split(' ');
            String[] strlist2 = str2.ToLower().Split(' ');
            int distance = 1000;
            foreach (String s1 in strlist1)
            {
                foreach (String s2 in strlist2)
                {
                    distance = Math.Min(distance, Utils.calculateLevenshteinDistance(s1, s2));
                }
            }

            return distance;
        }

        public String Consulta1(ArbolGeneral<DatoDistancia> arbol)
        {
            string resutl = "Implementar";
            return resutl;
        }


        public String Consulta2(ArbolGeneral<DatoDistancia> arbol)
        {
            string result = "Implementar";

            return result;
        }



        public String Consulta3(ArbolGeneral<DatoDistancia> arbol)
        {
            string result = "Implementar";

            return result;
        }

        // Ejercicio 1 — AgregarDato.
        public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia datoAInsertar)
        {
            // Si no hay árbol o dato, no hay nada que insertar.
            if (arbol == null || datoAInsertar == null)
            {
                return;
            }
            //Se toma el texto del nuevo dato y se lo normaliza una vez.
            string textoOriginal = datoAInsertar.texto;
            if (textoOriginal == null)
            {
                //Si el dato es nulo, duvuelve una cadena vacía
                textoOriginal = "";
            }

            //Se utiliza .trim() para eliminar los espacios en el principio y final de la cadena.
            //Se utiliza .ToLowerInvariant para convertirlo en minusculas.
            string textoNormalizadoAInsertar = textoOriginal.Trim().ToLowerInvariant();

            // Defino una FUNCIÓN LOCAL recursiva para encontrar el lugar correcto donde colocar un nuevo dato
            // Comienza en la raíz del árbol y se llama a sí misma en los nodos hijos
            // para descender por el árbol hasta encontrar un espacio.
            void InsertarEn(ArbolGeneral<DatoDistancia> nodoActual)
            {
                // Toma el texto del nodo actual el "padre" donde está parado.
                string textoPadre = nodoActual.getDatoRaiz().texto;
                if (textoPadre == null)
                {
                    textoPadre = "";
                }
                string textoPadreNormalizado = textoPadre.Trim().ToLowerInvariant();

                // Calcula la distancia Levenshtein entre el padre y el nuevo nodo.
                int distanciaEntrePadreYNuevo = Utils.calculateLevenshteinDistance(textoPadreNormalizado, textoNormalizadoAInsertar);
                // Recorro los hijos del nodo actual para verificar que no exista.
                foreach (var hijo in nodoActual.getHijos())
                {
                    int distanciaArista = hijo.getDatoRaiz().distancia;
                    if (distanciaArista == distanciaEntrePadreYNuevo)
                    {
                        // Si lo encuentra, baja a ese subárbol y sigue buscando.
                        InsertarEn(hijo);
                        return;
                    }
                }
                // Si no se encuentra un hijo a esa distancia, es el lugar correcto para insertar el nuevo dato.
                string descripcionOriginal = datoAInsertar.descripcion;
                if (descripcionOriginal == null)
                {
                    descripcionOriginal = "";
                }
                // El nuevo hijo guarda la distancia y los textos originales.
                var datoDelNuevoHijo = new DatoDistancia(distanciaEntrePadreYNuevo, textoOriginal, descripcionOriginal);
                // Cuelgo el nuevo hijo y listo: inserción completa.
                //Finalmente, se crea un nuevo nodo de árbol con el dato y se lo coloca como un nuevo hijo del nodo actual.
                nodoActual.agregarHijo(new ArbolGeneral<DatoDistancia>(datoDelNuevoHijo));
            }
            // Inicia la inserción recursiva desde la raiz del árbol.
            InsertarEn(arbol);
        }

        //Ejercicio2-Primer entrega.
        // El método "Buscar" recorre de forma recursiva un BK-tree para encontrar
        // todos los nodos cuyo texto se encuentre a una distancia menor o igual que el 'umbral'
        // del 'elementoABuscar'.
        public void Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> resultadosEncontrados)
        {
            // Validación, necesito un subárbol y una lista donde acumular resultados.
            //Si no los hay, no se retorna nada.
            if (arbol == null || resultadosEncontrados == null)
            {
                return;
            }

            //Se Normaliza la consulta y el texto del nodo actual.
            if (elementoABuscar == null)
            {
                elementoABuscar = "";
            }
            //Se utilizan .strim y .tolower del mismo modo que se hace en el primer metodo.
            string consultaNormalizada = elementoABuscar.Trim().ToLowerInvariant();
            string textoNodo = arbol.getDatoRaiz().texto;

            if (textoNodo == null)
            {
                textoNodo = "";
            }
            string nodoNormalizado = textoNodo.Trim().ToLowerInvariant();

            //Se calcula la distancia en el nodo actual.
            int distanciaAlNodoActual = Utils.calculateLevenshteinDistance(nodoNormalizado, consultaNormalizada);
            //Si la distancia está dentro del 'umbral', se lo  agrega a la lista de resultados.
            if (distanciaAlNodoActual <= umbral)
            {
                string textoOriginal = arbol.getDatoRaiz().texto;
                if (textoOriginal == null)
                {
                    textoOriginal = "";
                }
                string descOriginal = arbol.getDatoRaiz().descripcion;
                if (descOriginal == null)
                {
                    descOriginal = "";
                }
                resultadosEncontrados.Add(new DatoDistancia(distanciaAlNodoActual, textoOriginal, descOriginal));
            }
            //Poda del BK-tree. Solo se recorren los hijos que cumplen la condición del umbral.
            foreach (var hijo in arbol.getHijos())
            {
                //Se obtiene la distancia que esta guardada en el hijo.
                int distanciaArista = hijo.getDatoRaiz().distancia;

                // Si la distancia de la arista se encuentra dentro del rango, se baja recursivamente.
                if (distanciaArista >= distanciaAlNodoActual - umbral && distanciaArista <= distanciaAlNodoActual + umbral)
                {
                    Buscar(hijo, elementoABuscar, umbral, resultadosEncontrados);
                }
                // Si no cumple la condición, el subárbol se "poda" y no lo visitamos.
            }
        }
    }
}
