namespace SkyNET.Framework.Common.Dto {
    public record BaseModelEntryDto : EntryDto {
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя создавшего запись
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Дата последнего редактирования записи
        /// </summary>
        public DateTimeOffset? UpdatedDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя в последний раз отредактировавшего запись
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Дата установки отметки об удалении записи
        /// </summary>
        public DateTimeOffset? DeletedDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя установившего отметку об удалении записи
        /// </summary>
        public Guid? DeletedBy { get; set; }
    }
}
