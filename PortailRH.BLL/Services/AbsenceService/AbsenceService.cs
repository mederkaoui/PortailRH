using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Absence;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AbsenceService
{
    /// <summary>
    /// AbsenceService
    /// </summary>
    public class AbsenceService : IAbsenceService
    {
        /// <summary>
        /// IGenericRepository<Absence>
        /// </summary>
        private readonly IGenericRepository<Absence> _absenceRepository;

        /// <summary>
        /// IGenericRepository<TypeAbsence>
        /// </summary>
        private readonly IGenericRepository<TypeAbsence> _typeAbsenceRepository;

        /// <summary>
        /// IGenericRepository<TypeDocument>
        /// </summary>
        private readonly IGenericRepository<TypeDocument> _typeDocumentRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<AbsenceService>
        /// </summary>
        private readonly ILogger<AbsenceService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="absenceRepository">IGenericRepository<Absence></param>
        /// <param name="typeAbsenceRepository">IGenericRepository<TypeAbsence></param>
        /// <param name="unitOfWork">IUnitOfWork<TypeAbsence></param>
        /// <param name="typeDocumentRepository">IGenericRepository<TypeDocument><TypeAbsence></param>
        /// <param name="logger">ILogger<AbsenceService></param>
        public AbsenceService(IGenericRepository<Absence> absenceRepository, IGenericRepository<TypeAbsence> typeAbsenceRepository,
            IGenericRepository<TypeDocument> typeDocumentRepository, IUnitOfWork unitOfWork, ILogger<AbsenceService> logger)
        {
            _absenceRepository = absenceRepository;
            _typeAbsenceRepository = typeAbsenceRepository;
            _typeDocumentRepository = typeDocumentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get absences paginated list
        /// </summary>
        /// <param name="searchDto">AbsenceSearchDto</param>
        /// <returns>AbsencePaginatedListDto</returns>
        public async Task<AbsencePaginatedListDto> GetAbsencesPaginatedList(AbsenceSearchDto searchDto)
        {
            var absences = await _absenceRepository.GetPaginatedListAsync(
                                                                        currentPage: searchDto.CurrentPage,
                                                                        itemsPerPage: searchDto.ItemsPerPage,
                                                                        predicate: x => (searchDto.IdTypeAbsence == null || x.IdTypeAbsence == searchDto.IdTypeAbsence) &&
                                                                                    (x.CinEmploye.Contains(searchDto.SearchQuery ?? string.Empty) || string.Concat(x.CinEmployeNavigation.Prenom, " ", x.CinEmployeNavigation.Nom).Contains(searchDto.SearchQuery ?? string.Empty)),
                                                                        include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                                            .Include(x => x.IdDocumentNavigation)
                                                                                            .Include(x => x.IdTypeAbsenceNavigation)
                                                                    );

            return new AbsencePaginatedListDto
            {
                Absences = absences.Entities.Select(x => new AbsenceDto
                {
                    Id = x.Id,
                    DateDebut = x.DateDebut,
                    DateFin = x.DateFin,
                    TypeAbsence = x.IdTypeAbsenceNavigation.Nom,
                    Justifie = x.Justifie ?? false,
                    Justification = x.Justification,
                    CIN = x.CinEmploye,
                    Nom = x.CinEmployeNavigation.Nom,
                    Prenom = x.CinEmployeNavigation.Prenom,
                    DocumentNom = x.IdDocumentNavigation?.Nom
                }).ToArray(),
                Pagining = new PaginatedDto
                {
                    CurrentPage = absences.CurrentPage,
                    ItemsPerPage = absences.ItemsPerPage,
                    TotalItems = absences.TotalItems,
                    TotalPages = absences.TotalPages
                }
            };
        }

        /// <summary>
        /// Get Absence Types
        /// </summary>
        /// <returns>ICollection<AbsenceTypeDto></returns>
        public async Task<ICollection<AbsenceTypeDto>> GetAbsenceTypes()
        {
            var types = await _typeAbsenceRepository.GetListAsync();

            _logger.LogInformation("Get Absence Types");

            return types.Select(x => new AbsenceTypeDto
            {
                Id = x.Id,
                Nom = x.Nom
            }).ToList();
        }

        /// <summary>
        /// Delete Absence With the id from the parametres
        /// </summary>
        /// <param name="id">id absence</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task DeleteAbsence(int id)
        {
            var absence = await _absenceRepository.GetAsync(
                                                            predicate: x => x.Id == id,
                                                            disableTracking: false
                                                        )
                            ?? throw new InvalidDataException($"Il n'y a pas d'absence avec cet identifiant {id}");

            _absenceRepository.Delete(absence);

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Add Absence
        /// </summary>
        /// <param name="absenceDto">AddAbsenceDto</param>
        /// <returns></returns>
        public async Task AddAbsence(AddAbsenceDto absenceDto)
        {
            var idType = (await _typeDocumentRepository.GetAsync(x => x.Nom == "Certificat De Travail"))!.Id;

            await _absenceRepository.AddedAsync(new Absence
            {
                IdTypeAbsence = absenceDto.IdTypeAbsence,
                DateDebut = absenceDto.DateDebut,
                DateFin = absenceDto.DateFin,
                Justifie = absenceDto.Justifie,
                Justification = absenceDto.Justifie == true ? absenceDto.Justification : null,
                CinEmploye = absenceDto.CIN!,
                IdDocumentNavigation = absenceDto.DocumentNom != null ? new Document
                {
                    Nom = absenceDto.DocumentNom,
                    IdTypeDocument = idType,
                } : null
            });

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Get Detials Absence
        /// </summary>
        /// <param name="id">id absence</param>
        /// <returns>AbsenceDto</returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task<AbsenceDto> GetDetailsAbsence(int id)
        {
            var absence = await _absenceRepository.GetAsync(
                                                            predicate: x => x.Id == id,
                                                            include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                                .Include(x => x.IdDocumentNavigation)
                                                                                .Include(x => x.IdTypeAbsenceNavigation)
                                                        )
                            ?? throw new InvalidDataException($"Il n'y a pas d'absence avec cet identifiant {id}");

            return new AbsenceDto
            {
                Id = absence.Id,
                CIN = absence.CinEmploye,
                Nom = absence.CinEmployeNavigation.Nom,
                Prenom = absence.CinEmployeNavigation.Prenom,
                DateDebut = absence.DateDebut,
                DateFin = absence.DateFin,
                Justifie = absence.Justifie ?? false,
                Justification = absence.Justifie == true ? absence.Justification : null,
                TypeAbsence = absence.IdTypeAbsenceNavigation.Nom,
                DocumentNom = absence.IdDocumentNavigation?.Nom
            };
        }

        /// <summary>
        /// Get Employe Absences
        /// </summary>
        /// <param name="cin">cin employe</param>
        /// <returns>ICollection<EmployeAbsenceDto></returns>
        public async Task<ICollection<EmployeAbsenceDto>> GetEmployeAbsences(string cin)
        {
            var absences = await _absenceRepository.GetListAsync(
                                                                predicate: x => x.CinEmploye == cin,
                                                                include: inc => inc.Include(x => x.IdTypeAbsenceNavigation)
                                                                                    .Include(x => x.IdDocumentNavigation),
                                                                orderBy: ord => ord.OrderByDescending(x => x.DateDebut)
                                                            );

            return absences.Select(x => new EmployeAbsenceDto
            {
                Id = x.Id,
                DateDebut = x.DateDebut,
                DateFin = x.DateFin,
                Justifie = x.Justifie ?? false,
                TypeAbsence = x.IdTypeAbsenceNavigation.Nom,
            }).ToList();
        }

        /// <summary>
        /// Update absence informations
        /// </summary>
        /// <param name="id">id absence</param>
        /// <param name="editAbsenceDto">EditAbsenceDto</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task UpdateAbsence(int id, EditAbsenceDto editAbsenceDto)
        {
            var absence = await _absenceRepository.GetAsync(
                                                        predicate: x => x.Id == id,
                                                        include: inc => inc.Include(x => x.IdDocumentNavigation),
                                                        disableTracking: false
                                                    ) ?? throw new InvalidDataException($"Aucune absence avec l'id: {id}");

            var idType = (await _typeDocumentRepository.GetAsync(x => x.Nom == "Certificat De Travail"))!.Id;

            absence.DateDebut = editAbsenceDto.DateDebut;
            absence.DateFin = editAbsenceDto.DateFin;
            absence.IdTypeAbsence = editAbsenceDto.IdTypeAbsence;
            absence.CinEmploye = editAbsenceDto.CIN!;
            absence.Justifie = editAbsenceDto.Justifie;
            absence.Justification = editAbsenceDto.Justification;
            absence.IdDocumentNavigation = absence.IdDocumentNavigation?.Nom != editAbsenceDto.DocumentNom ? new Document
            {
                Nom = editAbsenceDto.DocumentNom,
                IdTypeDocument = idType
            } : absence.IdDocumentNavigation;

            _absenceRepository.Update(absence);

            await _unitOfWork.CommitAsync();
        }
    }
}
