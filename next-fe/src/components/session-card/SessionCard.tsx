import { Session } from "@/shared/types/Conference";
import "bootstrap/dist/css/bootstrap.css";
import './SessionCard.scss'

export type SessionCardProps = {
  session: Session;
};
export const SessionCard = ({ session }: SessionCardProps) => {
  return (
    <div className="card session-card">
      <div className="card-header">
        <a>
          <h4 className="card-title">{session.title}</h4>
        </a>
      </div>
      <div className="card-body">{session.abstract}</div>
      <div className="card-footer">
        {session.speakers?.map((speaker) => (
          <span key={speaker.id}>
            <em>
              <a>{speaker.name} </a>
            </em>
          </span>
        ))}
      </div>
    </div>
  );
};
