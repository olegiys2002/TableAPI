using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IMapper
    {
        UserDTO ToDTO(User user);
        TableDTO ToDTO(Table table);
        User ToDomainModel(UserForCreationDTO userForCreationDTO);
        Table ToDomainModel(TableForCreationDTO tableForCreationDTO);
        List<UserDTO> ToListDTO(List<User> users);
        List<TableDTO> ToListDTO(List<Table> tables);

    }
}
