using Core.Application.Pipelines.Transactional;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;

public record CreateMediaFileObjectCommand : IRequest<string>, ITransactional
{
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public long Size { get; set; }
    public Guid? UploadedByUserId { get; init; }
    public bool Status { get; set; }
}