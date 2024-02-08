using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.Norh,
            Direction.South,
            Direction.West,
            Direction.East,
            Direction.SouthEast,
            Direction.SouthWest,
            Direction.NorthEast,
            Direction.NorthWest
        };
        public King(Player color)
        {
            Color = color;
        }

        private static bool IsUnmovedRook(Position pos, Board board)
        {
            if (board.IsEmpty(pos))
            {
                return false;
            }

            Piece piece = board[pos];
            return piece.Type == PieceType.Rook && !piece.HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> position, Board board)
        {
            return position.All(pos => board.IsEmpty(pos));
        }

        private bool CanCastleKingSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 7);
            Position[] betweenPositions = new Position[] { new(from.Row, 5), new(from.Row, 6) };

            return IsUnmovedRook(rookPos, board)&& AllEmpty(betweenPositions, board);
        }
        private bool CanCatsleQueenSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 0);
            Position[] beetweenPositions= new Position[] { new(from.Row,1), new(from.Row,2), new(from.Row,3) };
            return IsUnmovedRook(rookPos, board) && AllEmpty(beetweenPositions, board);
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.IsInside(to))
                {
                    continue;
                }
                if(board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach(Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }

            if(CanCastleKingSide(from, board))
            {
                yield return new Castle(MoveType.CastleKS, from);
            }
            if(CanCatsleQueenSide(from, board))
            {
                yield return new Castle(MoveType.CastleQS, from);
            }

        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
