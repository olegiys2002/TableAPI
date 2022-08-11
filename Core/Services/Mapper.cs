﻿using Core.DTOs;
using Core.IServices;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class Mapper : IMapper
    {
        public User ToDomainModel(UserForCreationDTO userForCreationDTO)
        {
            User user = new User()
            {
                Name = userForCreationDTO.Name,
                Role = userForCreationDTO.Role,
            };
            return user;
        }

        public Table ToDomainModel(TableForCreationDTO tableForCreationDTO)
        {
            Table table = new Table()
            {
                CountOfSeats = tableForCreationDTO.CountOfSeats,
                Number = tableForCreationDTO.Number,

            };
            return table;
        }
        public TableDTO ToDTO(Table table)
        {
            TableDTO tableDTO = new TableDTO()
            {
                Id = table.Id,
                CountOfSeats = table.CountOfSeats,
                CreatedAt = table.CreatedAt,    
                UpdatedAt = table.UpdatedAt,
                Number = table.Number
            };
            return tableDTO;
        }

        public UserDTO ToDTO(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Name = user.Name,
                Role = user.Role
            };
            return userDTO;
        }

        public List<UserDTO> ToListDTO(List<User> users)
        {
            List<UserDTO> userDTOs = new();

            foreach (var user in users)
            {
                UserDTO userDTO = ToDTO(user);
                userDTOs.Add(userDTO);
            }
            return userDTOs;
        }
        public List<TableDTO> ToListDTO(List<Table> tables)
        {
            List<TableDTO> tableDTOs = new List<TableDTO>();

            foreach(var table in tables)
            {
                TableDTO tableDTO = ToDTO(table);
                tableDTOs.Add(tableDTO);
            }
            return tableDTOs;
        }
    }
}
