import { Session } from "@/shared/types/Conference";
import "bootstrap/dist/css/bootstrap.css";
import Link from "next/link";

export type SessionCardProps = {
  session: Session;
};
export const SessionCard = ({ session }: SessionCardProps) => {
  return (
    <div className="card session-card">
      <div className="card-header">
        <Link href="/sessions/[id]" as={`/sessions/${session.id}`}>
          <h4 className="card-title">{session.title}</h4>
        </Link>
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
