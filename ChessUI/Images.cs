using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/pawn_white.png") },
            {PieceType.Bishop, LoadImage("Assets/bishop_white.png") },
            {PieceType.Knight, LoadImage("Assets/knight_white.png") },
            {PieceType.Rook, LoadImage("Assets/rook_white.png") },
            {PieceType.King, LoadImage("Assets/king_white.png") },
            {PieceType.Queen, LoadImage("Assets/queen_white.png") },
        };
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/pawn_black.png") },
            {PieceType.Bishop, LoadImage("Assets/bishop_black.png") },
            {PieceType.Knight, LoadImage("Assets/knight_black.png") },
            {PieceType.Rook, LoadImage("Assets/rook_black.png") },
            {PieceType.King, LoadImage("Assets/king_black.png") },
            {PieceType.Queen, LoadImage("Assets/queen_black.png") },
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath,UriKind.Relative));
        }
        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece piece)
        {
            if(piece == null)
            {
                return null;
            }
            return GetImage(piece.Color, piece.Type);
        }
    }
}
