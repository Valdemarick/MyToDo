﻿using MyToDo.Application.Common.Dtos.Common;

namespace MyToDo.Application.Common.Dtos.Members;

public class MemberPagedListDto : BasePagedListDto<MemberDto>
{
    public MemberPagedListDto(List<MemberDto> items, int totalCount, int pageIndex, int pageSize) 
        : base(items, totalCount, pageIndex, pageSize)
    {
    }

    public MemberPagedListDto()
    {
    }
}
