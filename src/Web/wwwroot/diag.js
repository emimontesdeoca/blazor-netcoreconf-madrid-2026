// Utilidades de medida para la demo. Nada de esto es necesario en una app real.
window.diag = {
    // Cuántos elementos hay de verdad en el DOM para un selector (demo 3: filas de la tabla).
    contar: (selector) => document.querySelectorAll(selector).length,

    medirFilas: (contenedor) => new Promise((resolve, reject) => {
        let cuadro;
        let anterior = -1;
        const limite = setTimeout(() => {
            cancelAnimationFrame(cuadro);
            reject(new Error("No se han mostrado datos en la lista en 10 segundos."));
        }, 10000);
        const medir = () => {
            if (!contenedor.isConnected) {
                clearTimeout(limite);
                reject(new Error("La lista se ha desmontado antes de terminar la medición."));
                return;
            }
            const filas = contenedor.querySelectorAll("tbody tr[data-pedido]").length;
            if (filas > 0 && filas === anterior) {
                clearTimeout(limite);
                resolve(filas);
                return;
            }
            anterior = filas;
            cuadro = requestAnimationFrame(medir);
        };
        cuadro = requestAnimationFrame(medir);
    }),

    // Escribe un texto letra a letra en un input, disparando el evento "input" que escucha @oninput (demo 2).
    escribir: async (selector, texto, intervaloMs) => {
        const el = document.querySelector(selector);
        if (!el) throw new Error(`No se encuentra el buscador: ${selector}`);
        el.focus();
        el.value = "";
        el.dispatchEvent(new Event("input", { bubbles: true }));
        for (const letra of texto) {
            await new Promise(r => setTimeout(r, intervaloMs));
            el.value += letra;
            el.dispatchEvent(new Event("input", { bubbles: true }));
        }
    }
};
