using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Usuario>.GetAll();
                
                var roles = Crud<Rol>.GetAll();
                var tipos = Crud<TipoIdentificacion>.GetAll();
                var generos = Crud<Genero>.GetAll();

                foreach(var item in list) {
                    item.Rol = Enumerable.FirstOrDefault(roles, r => r.Id == item.IdRol);
                    item.TipoIdentificacion = Enumerable.FirstOrDefault(tipos, t => t.Id == item.IdTipoIdentificacion);
                    item.Genero = Enumerable.FirstOrDefault(generos, g => g.Id == item.IdGenero);
                }
                
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Usuario>());
            }
        }

        // GET: Usuarios/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Usuario>.GetById(id);
                if (item == null) return NotFound();

                // Asociar manualmente las relaciones si es necesario
                item.Rol = Crud<Rol>.GetById(item.IdRol);
                item.TipoIdentificacion = Crud<TipoIdentificacion>.GetById(item.IdTipoIdentificacion);
                item.Genero = Crud<Genero>.GetById(item.IdGenero);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(Crud<Rol>.GetAll(), "Id", "NombreRol");
            ViewBag.TiposIdentificacion = new SelectList(Crud<TipoIdentificacion>.GetAll(), "Id", "NombreTipo");
            ViewBag.Generos = new SelectList(Crud<Genero>.GetAll(), "Id", "NombreGenero");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Create(Usuario item)
        {
            // Asignar contraseña por defecto (BIOU + NumeroIdentificacion)
            ModelState.Remove("ContrasenaHash");
            item.ContrasenaHash = "BIOU" + item.NumeroIdentificacion;

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = Crud<Usuario>.Create(item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Usuario creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al guardar el Usuario en la base de datos (Error 400/500).";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            ViewBag.Roles = new SelectList(Crud<Rol>.GetAll(), "Id", "NombreRol", item.IdRol);
            ViewBag.TiposIdentificacion = new SelectList(Crud<TipoIdentificacion>.GetAll(), "Id", "NombreTipo", item.IdTipoIdentificacion);
            ViewBag.Generos = new SelectList(Crud<Genero>.GetAll(), "Id", "NombreGenero", item.IdGenero);
            return View(item);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Usuario>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.Roles = new SelectList(Crud<Rol>.GetAll(), "Id", "NombreRol", item.IdRol);
                ViewBag.TiposIdentificacion = new SelectList(Crud<TipoIdentificacion>.GetAll(), "Id", "NombreTipo", item.IdTipoIdentificacion);
                ViewBag.Generos = new SelectList(Crud<Genero>.GetAll(), "Id", "NombreGenero", item.IdGenero);
                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Edit(int id, Usuario item)
        {
            if (id != item.Id) return BadRequest();

            // Si la contraseña viene vacía en la vista, significa que no la quieren cambiar
            if (string.IsNullOrEmpty(item.ContrasenaHash))
            {
                ModelState.Remove("ContrasenaHash");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = Crud<Usuario>.GetById(id);
                    if (existingUser == null) return NotFound();

                    if (string.IsNullOrEmpty(item.ContrasenaHash))
                    {
                        // Conservar contraseña anterior
                        item.ContrasenaHash = existingUser.ContrasenaHash;
                    }
                    else
                    {
                        // Hashear nueva contraseña
                        using (var md5 = System.Security.Cryptography.MD5.Create())
                        {
                            var inputBytes = System.Text.Encoding.UTF8.GetBytes(item.ContrasenaHash);
                            var hashBytes = md5.ComputeHash(inputBytes);
                            item.ContrasenaHash = Convert.ToHexString(hashBytes).ToLower();
                        }
                    }

                    bool isSuccess = Crud<Usuario>.Update(id, item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Usuario actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al actualizar el Usuario.";
                    }
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            ViewBag.Roles = new SelectList(Crud<Rol>.GetAll(), "Id", "NombreRol", item.IdRol);
            ViewBag.TiposIdentificacion = new SelectList(Crud<TipoIdentificacion>.GetAll(), "Id", "NombreTipo", item.IdTipoIdentificacion);
            ViewBag.Generos = new SelectList(Crud<Genero>.GetAll(), "Id", "NombreGenero", item.IdGenero);
            return View(item);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Usuario>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<Usuario>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

